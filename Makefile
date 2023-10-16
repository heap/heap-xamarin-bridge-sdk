export NUGET_ENABLE_LEGACY_CSPROJ_PACK := true

PUBLIC_REPO := git@github.com:heap/heap-xamarin-bridge-sdk.git
INTERNAL_REPO := git@github.com:heap/heap-xamarin-bridge.git
MAIN_BRANCH := main

define set_public_repo

	if [ "$$(git remote get-url --push origin)" != '${INTERNAL_REPO}' ]; then \
		echo 'Incorrect origin. Aborting.'; \
		exit 1; \
	elif [ "$$(git remote get-url --push public)" = "" ]; then \
		git remote add public '${PUBLIC_REPO}'; \
	elif [ "$$(git remote get-url --push public)" != '${PUBLIC_REPO}' ]; then \
		git remote set-url --push public '${PUBLIC_REPO}'; \
	fi

endef

dependencies:
# (LOCAL) Install the dependencies required to run build steps.

	@echo "--- Installing msbuild and dependencies"

	curl -O curl -O https://download.mono-project.com/archive/6.12.0/macos-10-universal/MonoFramework-MDK-6.12.0.182.macos10.xamarin.universal.pkg
	curl -O https://download.visualstudio.microsoft.com/download/pr/5f9ea8f6-0afe-46b3-b8e8-5dee4c2dd14c/b357a2b833ba86598aaff58fc013f06c/xamarin.ios-16.2.0.5.pkg
	curl -O https://download.visualstudio.microsoft.com/download/pr/5f9ea8f6-0afe-46b3-b8e8-5dee4c2dd14c/49f71359b7d9818b689414c1f0e52e91/xamarin.android-13.2.0.0.pkg
	installer -pkg MonoFramework-MDK-6.12.0.182.macos10.xamarin.universal.pkg -target /
	installer -pkg xamarin.ios-16.2.0.5.pkg -target /
	installer -pkg xamarin.android-13.2.0.0.pkg -target /
	rm MonoFramework-MDK-6.12.0.182.macos10.xamarin.universal.pkg
	rm xamarin.ios-16.2.0.5.pkg
	rm xamarin.android-13.2.0.0.pkg

	brew install openjdk@11
	brew install --cask android-commandlinetools

	ln -sfn /usr/local/opt/openjdk@11/libexec/openjdk.jdk /Library/Java/JavaVirtualMachines/openjdk-11.jdk

clean:
# (LOCAL or CI) Clears all artifacts from the build.

	-rm -r artifacts
	-rm -r */bin
	-rm -r */obj

bridge:
# (LOCAL or CI) Builds the common bridge library.

	@echo "--- Building bridge"
	mkdir -p artifacts
	msbuild -r -p:Configuration=Release HeapInc.Xamarin
	nuget pack HeapInc.Xamarin -Prop Configuration=Release -OutputDirectory ./artifacts
	cp HeapInc.Xamarin/bin/Release/netstandard2.1/HeapInc.*.dll artifacts

ios:
# (LOCAL or CI) Builds the iOS wrapper for heap-swift-core.

	@echo "--- Building for iOS"
	mkdir -p artifacts
	msbuild -r -p:Configuration=Release HeapInc.Xamarin.iOS
	nuget pack HeapInc.Xamarin.iOS -Prop Configuration=Release -OutputDirectory ./artifacts
	cp HeapInc.Xamarin.iOS/bin/Release/HeapInc.*.dll artifacts

android:
# (LOCAL or CI) Builds the Android wrapper for heap-android-core.

	@echo "--- Building for Android"
	mkdir -p artifacts
	msbuild -r -p:Configuration=Release HeapInc.Xamarin.Android
	nuget pack HeapInc.Xamarin.Android -Prop Configuration=Release -OutputDirectory ./artifacts
	cp HeapInc.Xamarin.Android/bin/Release/HeapInc.*.dll artifacts

push_branch_to_public:
# (CI) Pushes the current branch to the public repo if it is `main`.
# This can be run locally if it is failing on the CDN.

ifndef BUILDKITE_BRANCH
	$(error BUILDKITE_BRANCH is not set)
endif

ifneq (${BUILDKITE_BRANCH},${MAIN_BRANCH})
	$(error Current branch ${BUILDKITE_BRANCH} is not ${MAIN_BRANCH})
endif

	$(call set_public_repo)

	git fetch origin '${MAIN_BRANCH}:${MAIN_BRANCH}'
	git push public '${MAIN_BRANCH}'

push_tag_to_public:
# (CI) Pushes the current tag to the public repo.
# This can be run locally if it is failing on the CDN.

ifndef BUILDKITE_TAG
	$(error BUILDKITE_TAG is not set)
endif

	$(call set_public_repo)

	git fetch origin --tags
	git push public ${BUILDKITE_TAG}

reset_local_nuget:
	nuget sources remove -Name 'HeapInc Local' 2> /dev/null || true
	nuget sources Add -Name 'HeapInc Local' -Source $$PWD/artifacts
	rm -rf ~/.nuget/packages/heapinc.xamarin* || true

sync_example_versions:
	./DevTools/SyncExampleVersions.sh

restore_examples: bridge ios android reset_local_nuget sync_example_versions
	nuget restore examples.sln -Source $$PWD/artifacts

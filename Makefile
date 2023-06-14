dependencies:

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
	-rm -r artifacts
	-rm -r */bin
	-rm -r */obj

bridge:
	@echo "--- Building bridge"
	msbuild -r -p:Configuration=Release HeapInc.Xamarin
	mkdir -p artifacts
	cp HeapInc.Xamarin/bin/Release/netstandard2.1/*.dll artifacts

ios:
	@echo "--- Building for iOS"
	msbuild -r -p:Configuration=Release HeapInc.Xamarin.iOS
	mkdir -p artifacts
	cp HeapInc.Xamarin.iOS/bin/Release/*.dll artifacts

android:
	@echo "--- Building for Android"
	msbuild -r -p:Configuration=Release HeapInc.Xamarin.Android
	mkdir -p artifacts
	cp HeapInc.Xamarin.Android/bin/Release/*.dll artifacts

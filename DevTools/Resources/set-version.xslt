<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="xml" version="1.0" encoding="utf-8" indent="yes"/>
  <xsl:param name="package" />
  <xsl:param name="version" />
  <xsl:template match="node()|@*">
    <xsl:choose>
      <xsl:when
        test="name(..) = 'Version' and name(../..) = 'PackageReference' and ../../@Include = $package">
        <xsl:value-of select="$version" />
      </xsl:when>
      <xsl:otherwise>
        <xsl:copy>
          <xsl:apply-templates select="@*|node()" />
        </xsl:copy>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
</xsl:stylesheet>

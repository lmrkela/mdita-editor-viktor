<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:ditamsg="http://dita-ot.sourceforge.net/ns/200704/ditamsg"
	xmlns:related-links="http://dita-ot.sourceforge.net/ns/200709/related-links"
	xmlns:functx="http://www.functx.com">
	
	<xsl:template match="*[contains(@class, ' preface ')]">
		<xsl:value-of select="'10131990'"/>
<!--		<xsl:call-template name="add-directory"/>-->
	</xsl:template>
	
	<xsl:template name="add-directory">
		<xsl:for-each select="*[contains(@class, ' bookmap/chapter ' )]">
			<xsl:value-of select="child::*[contains(@class, ' topic/navtitle ' )]"/>
		</xsl:for-each>
	</xsl:template>
	
</xsl:stylesheet>
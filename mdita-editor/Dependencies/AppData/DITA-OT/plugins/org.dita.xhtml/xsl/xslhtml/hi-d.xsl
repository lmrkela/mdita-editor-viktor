<?xml version="1.0" encoding="UTF-8" ?>
<!-- This file is part of the DITA Open Toolkit project hosted on 
     Sourceforge.net. See the accompanying license.txt file for 
     applicable licenses.-->
<!-- (c) Copyright IBM Corp. 2004, 2005 All Rights Reserved. -->

<xsl:stylesheet version="1.0"
     xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<!-- hi-d.ent Phrase domain: b | i | u | tt | sup | sub -->

<xsl:template match="*[contains(@class,' hi-d/b ')]" name="topic.hi-d.b">
<xsl:if test="node()">
 <strong>
  <xsl:call-template name="commonattributes"/>
  <xsl:call-template name="setidaname"/>
  <xsl:apply-templates/>
  </strong>
</xsl:if>
</xsl:template>

<xsl:template match="*[contains(@class,' hi-d/i ')]" name="topic.hi-d.i">
<xsl:if test="node()">
 <em>
  <xsl:call-template name="commonattributes"/>
  <xsl:call-template name="setidaname"/>
  <xsl:apply-templates/>
  </em>
</xsl:if>
</xsl:template>

<xsl:template match="*[contains(@class,' hi-d/u ')]" name="topic.hi-d.u">
 <xsl:if test="node()">
 <u>
  <xsl:call-template name="commonattributes"/>
  <xsl:call-template name="setidaname"/>
  <xsl:apply-templates/>
  </u>
 </xsl:if>
</xsl:template>

<xsl:template match="*[contains(@class,' hi-d/tt ')]" name="topic.hi-d.tt">
 <tt>
  <xsl:call-template name="commonattributes"/>
  <xsl:call-template name="setidaname"/>
  <xsl:apply-templates/>
  </tt>
</xsl:template>

<xsl:template match="*[contains(@class,' hi-d/sup ')]" name="topic.hi-d.sup">
 <sup>
  <xsl:call-template name="commonattributes"/>
  <xsl:call-template name="setidaname"/>
  <xsl:apply-templates/>
 </sup>
</xsl:template>

<xsl:template match="*[contains(@class,' hi-d/sub ')]" name="topic.hi-d.sub">
 <sub>
  <xsl:call-template name="commonattributes"/>
  <xsl:call-template name="setidaname"/>
  <xsl:apply-templates/>
  </sub>
</xsl:template>

</xsl:stylesheet>

<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
		<html> 
			<body>
				<table border = "1">
					<TR>
						<th>Style</th>
						<th>Level</th>
						<th>Day</th>
						<th>Time</th>
						<th>Teacher</th>
						<th>Hall</th>
					</TR>
					<xsl:for-each select = "DanceStudio/Dance">
						<tr>
							<td>
								<xsl:value-of select = "@Style"/>
							</td>
							<td>
								<xsl:value-of select = "@Level"/>
							</td>
							<td>
								<xsl:value-of select = "@Day"/>
							</td>
							<td>
								<xsl:value-of select = "@Time"/>
							</td>
							<td>
								<xsl:value-of select = "@Teacher"/>
							</td>
							<td>
								<xsl:value-of select = "@Hall"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
    </xsl:template>
</xsl:stylesheet>

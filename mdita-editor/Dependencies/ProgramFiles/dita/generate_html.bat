setlocal EnableDelayedExpansion

set DITA_DIR=%1%
set INPUT_MAP=%2%
set HTML_DIR=%3%
set OUTPUT_DIR=%4%

set JAVA_HOME=jre7

set ANT_HOME=ant
set ANT_OPTS=-Xmx512m %ANT_OPTS% -Djavax.xml.transform.TransformerFactory=net.sf.saxon.TransformerFactoryImpl

set PATH=%ANT_HOME%\bin;%JAVA_HOME%\bin;%PATH%
set CLASSPATH=dita\saxon9.jar;dita\saxon9-dom.jar;dita\xercesImpl.jar;dita\xml-apis.jar;dita;dita\dost.jar;dita\commons-codec-1.4.jar;dita\resolver.jar;dita\icu4j.jar;%CLASSPATH%

"%JAVA_HOME%\bin\java" -Xmx1024m -Xms512m -jar "dita\dost.jar" /i:%INPUT_MAP% /transtype:xhtml /basedir:%HTML_DIR%\ /outdir:%OUTPUT_DIR% /ditadir:%DITA_DIR%\ /copycss:no /cssroot:%HTML_DIR%\css\ /css:style.css /ftr:%HTML_DIR%\userhf\footer.xml /filter:%HTML_DIR%\PubDitaVal.ditaval /csspath:../../../css 2>> %OUTPUT_DIR%\DitaConvert.log
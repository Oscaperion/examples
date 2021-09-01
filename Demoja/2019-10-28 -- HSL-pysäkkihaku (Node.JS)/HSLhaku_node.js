/*
    Node.js-demo - HSL-pys‰kkihaku
    Oskari Oksanen, 28.10.2019
*/

// const fs = require('fs');
const url = require('url');
const http = require('http');
const XMLHttpRequest_node = require("xmlhttprequest").XMLHttpRequest;

// JSON-data tallennetaan t‰h‰n
var rawdata; // = fs.readFileSync('HSL_n_pysakit-syksy19.geojson');

// T‰m‰ hankkii JSON-tiedoston HSL:n sivuilta
var requ = new XMLHttpRequest_node();
requ.onreadystatechange = function() {
   if (requ.readyState == 4 && requ.status == 200){
      //alert(req.responseText);
      rawdata = requ.responseText;
      console.log("JSON succesfully fetched from HSL's site");
   }
};
requ.open("GET", "https://opendata.arcgis.com/datasets/b2aa879ce93c4068ac63b64d71f24947_0.geojson", false);
requ.send(null);

// K‰sitell‰‰n JSON-tiedostoa ja muutetaan sen tiedot listaukselle sopiviksi
var parsedJson = JSON.parse(rawdata);
const feats = parsedJson.features;

// Alustetaan muuttuja rivinvaihdolle
const rn =  '\r\n';

// Alustetaan HTML-koodia sivujen alkuun
var htmlStrBegin = '<!DOCTYPE html>' + rn;
htmlStrBegin += '<html>' + rn;
htmlStrBegin += '<head>' + rn;
htmlStrBegin += '<meta http-equiv="Content-Type" content="text/html; charset=utf-8">' + rn;
htmlStrBegin += '<title>Node.js-demo - HSL-pys&auml;kkihaku</title>' + rn;
htmlStrBegin += '</head>' + rn;
htmlStrBegin += '<body>' + rn;
htmlStrBegin += '<h2>Node.js-demo - HSL-pys&auml;kkihaku</h2>' + rn;
htmlStrBegin += '<hr/>' + rn;

// Alustetaan HTML-koodia index.html-sivua varten
var htmlStrIndex = '<p>' + rn + 'Etsi pys&auml;kki&auml; nimen tai tunnuksen perusteella:' + rn;
htmlStrIndex += '<form action="results.html" method="GET">' + rn;
htmlStrIndex += '<input type="text" name="searchWord" />&nbsp;' + rn;
htmlStrIndex += '<input type="submit" value="Etsi" />' + rn;
htmlStrIndex += '</form><br/>' + rn + '</p>' + rn + '</body>' + rn + '</html>';

// Alustetaan HTML-koodia results.html-sivun alkuun ja loppuun
var htmlStrResult1 = '<b><a href="index.html">&#8656;&nbsp;Palaa</a></b>' + rn;
var htmlStrResult2 = '<hr/><b><a href="index.html">&#8656;&nbsp;Palaa</a></b>' + rn;
htmlStrResult2 += '</body></html>';

// T‰ll‰ funktiolla luodaan lista pys‰keist‰ esitett‰v‰ksi results.html-sivulle hakusanan (searchWord) perusteella
function createList(searchWord) {
   var i;
   var rStr = '<div>' + rn;
   var matchFound = false;

   for (i = 0; i < feats.length; i++) {
       var props = feats[i].properties;
       var compStr = props.NIMI1 + ', ' + props.NIMI2 + ' ' + props.NAMN1 + ', ' + props.NAMN2 + ' ' + props.LYHYTTUNNU + ' (' + props.SOLMUTUNNU + ')';

       if (compStr.toLowerCase().indexOf(searchWord.trim().toLowerCase()) > -1) {
           matchFound = true;

           rStr += '<hr/>' + rn + '<div>Pys&auml;kki ';

           if (props.LYHYTTUNNU.trim().length > 0) {
               rStr += props.LYHYTTUNNU + ' (' + props.SOLMUTUNNU + '):<br/>' + rn;
           } else {
               rStr += props.SOLMUTUNNU + ':<br/>' + rn;
           }

           rStr += props.NIMI1 + ', ' + props.NIMI2 + '<br/>' + rn;
           rStr += props.NAMN1 + ', ' + props.NAMN2 + rn + '</div>' + rn;
       }
   }
   
   if (!matchFound) {
       rStr += '<hr/>Ei l&ouml;ytynyt pys&auml;kki&auml; hakusanalla: <code>' + searchWord + '</code>';
   }

   console.log('List made succesfully!');

   rStr += '</div>' + rn;

   return rStr;
}

http.createServer(function (req, res) {
  var q = url.parse(req.url, true);
  //const alku = '/HSLhaku_nodejs';

  /*
  console.log(q.pathname);//.substr(1));
  console.log(q.query);
  console.log(q.search);
  console.log(q.query.season);
  console.log(q.query.season === undefined); 
  */
  
  if ('/HSLhaku_nodejs/results.html'.localeCompare(q.pathname) == 0) {
     res.writeHead(200, {'Content-Type': 'text/html'});
     
     res.write(htmlStrBegin);
     res.write(htmlStrResult1);
     // res.write('Hello World!<br/>');
     res.write(createList(q.query.searchWord));
     res.write(htmlStrResult2);

     res.end();
  }

  if ('/HSLhaku_nodejs'.localeCompare(q.pathname) == 0 || ( '/HSLhaku_nodejs/').localeCompare(q.pathname) == 0 || ('/HSLhaku_nodejs/index.html').localeCompare(q.pathname) == 0) {
     res.writeHead(200, {'Content-Type': 'text/html'});
     
     res.write(htmlStrBegin);
     res.write(htmlStrIndex);

     res.end();
  } else {
      res.writeHead(404, {'Content-Type': 'text/html'});
      res.end("404 Not Found.");
  }

  // });
}).listen(8080);
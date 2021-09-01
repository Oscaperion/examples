/*
    AngularJS-demo - HSL-pysäkkihaku (jsonParser.js)
    Oskari Oksanen, 16.10.2019 
*/


var app = angular.module('stopLister', []);

app.controller('stopController',
   function ($scope, $http) {
        var request = {
            method: 'get',
            url: 'https://opendata.arcgis.com/datasets/b2aa879ce93c4068ac63b64d71f24947_0.geojson',
            dataType: 'json',
            contentType: "application/json"
        };
 
        $scope.searchString = getParameterByName('search').trim(); 
        $scope.stopsData = new Array;
 
        $http(request)
            .success(function (jsonData) {
                //$scope.stopsData = jsonData;
                //$scope.list = $scope.stopsData;
                //$scope.editedList = createList($scope.stopsData, $scope.searchString);
                
                $scope.stopsData = createList(jsonData, $scope.searchString);
                
                console.log('Search word: \"' + $scope.searchString + '\"');
            })
            .error(function () {
                $scope.stopsData = createList('Lollerson dawson', $scope.searchString)
                console.log('JSON-tiedosto ei ollut saatavilla.');
            });
    }
);
        
// Tarkistetaan, löytyykö hakusanaa minkään pysäkin tiedoista. Mikäli hakusana löytyy edes kerran, palautetaan funktiolle annettu lista pysäkeistä. Mikäli ei löydy, palautetaan yksi NULL-tiedoilla täytetty pysäkki
function createList(ogList, searchWord) {
    // Alustusta for-loopia varten
    var feats = ogList.features;
    var swLowercase = searchWord.toLowerCase();
    var i;
    
    for (i = 0; i < feats.length; i++) {
        var tmp = feats[i].properties;
        var comparisonStr = tmp.NIMI1 + ', ' + tmp.NIMI2 + ' ' + tmp.NAMN1 + ', ' + tmp.NAMN2 + ' ' + tmp.LYHYTTUNNU + ' (' + tmp.SOLMUTUNNU + ')';
        var includesSearchWord = (comparisonStr.toLowerCase()).indexOf(swLowercase);
        
        // Tarkistetaan löytyykö hakusanaa. Jos löytyy palautetaan suoraan funktiolle annettu lista
        if (includesSearchWord > -1) {
            console.log('Found first match: ' + (i + 1) + '. ' + tmp.LYHYTTUNNU + ': ' + tmp.NIMI1 + ', ' + tmp.NIMI2 + ' (' + tmp.NAMN1 + ', ' + tmp.NAMN2 + ')');
            return ogList;
        }
    }
    
    // Mikäli hakusanaa ei löytynyt, tästä eteenpäin luodaan ja palautetaan sen mukainen yhden "pysäkin" lista.
    console.log('Match not found by searching ' + searchWord);
    
    var notMatchFound = {"features":[{"properties":{"FID":"NULL","SOLMUTUNNU":"NULL","LYHYTTUNNU":"NULL","NIMI1":"NULL","NAMN1":"NULL","NIMI2":"NULL","NAMN2":"NULL"}}]};
    
    return notMatchFound; 
}
        
// Tämä tarkistaa onko sivulle annettu muuttujia osoiterivin kautta ja sellaisten tietojen löytyessä palauttaa annetut tiedot
function getParameterByName(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(window.location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}
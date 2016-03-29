(function () {
    var app = angular.module('palindroom-app', []);

    app.controller('PalindroomController', function ($scope) {
        // scope variables
        var woord = '';
        var klaar = false;
        var actieveRij = 0;
        var eindstring;

        $scope.lengte = 1;
        $scope.pal = '';
        $scope.woord = ''; 
        //$scope.woorden = [];
        $scope.aantal = 0;
        $scope.palindroomHub = null; 

        $scope.palindroomHub = $.connection.palindroomHub; // initializes hub
        $.connection.hub.start(); // starts hub

        // register a client method on hub to be invoked by the server

        $scope.palindroomHub.client.broadcastAantal = function (aantal) {
            var newAantal = aantal;
            $scope.aantal = newAantal;
            $scope.$apply();
        };
        $scope.newAantal = function () {
            $scope.palindroomHub.server.sendAantal($scope.aantal);
        };

        $scope.palindroomHub.client.broadcastLengte = function (lengte) {
            var newLengte = lengte;
            $scope.lengte = newLengte;
            $scope.$apply();
        };
        $scope.newLengte = function () {
            $scope.palindroomHub.server.sendLengte($scope.lengte);
        };

        $scope.palindroomHub.client.broadcastWoord = function (woord) {
            var newWoord = woord;
            // push the newly coming message to the collection of messages
            //$scope.woorden.push(newWoord);
            $scope.woord = newWoord;
            $scope.$apply();
        };
        $scope.newWoord = function () {
            // sends a new message to the server
            $scope.aantal = 0;
            $scope.palindroomHub.server.sendAantal($scope.aantal);
            $scope.palindroomHub.server.sendWoord($scope.woord);
            if ($scope.lengte == 0)
            {
                $scope.lengte = 1;
            }
            $scope.palindroomHub.server.sendLengte($scope.lengte);
            // maak eerste woord
            woord = 'a'.repeat($scope.lengte);
            $scope.woord = woord;
            
            //console.log(woord);
            eindstring = 'z'.repeat($scope.lengte);
            //console.log(eindstring);
            while (!klaar) {
                //console.log('in while loop');
                $scope.woord = maakVolgendePalindroom();
                $scope.palindroomHub.server.sendWoord($scope.woord);
                //$scope.palindroomHub.server.sendAantal($scope.aantal);
            }
            $scope.woord = '';
            $scope.woorden = [];
            klaar = false;
            actieveRij = 0;
            $scope.palindroomHub.server.sendAantal($scope.aantal);
        }

        function volgendeLetter(oudeletter) {
            // haal de volgende letter op
            var nieuweletter = 'a';
            if (oudeletter != 'z') {
                nieuweletter = String.fromCharCode(oudeletter.charCodeAt(oudeletter.length - 1) + 1)
            }
            return nieuweletter;
        }

        function draaiNieuweWoord(letter) {
            var eerstestuk;
            var nieuweWoord = woord;
            eerstestuk = nieuweWoord.substring(0, actieveRij).concat(letter).concat(nieuweWoord.substring(actieveRij + 1, nieuweWoord.length));
            nieuweWoord = eerstestuk.substring(0, eerstestuk.length - actieveRij - 1).concat(letter).concat(eerstestuk.substring(eerstestuk.length - actieveRij, eerstestuk.length));
            return nieuweWoord;
        }

        function maakVolgendePalindroom() {
            //var nieuweWoord;
            var volgende;
            var vorigeWoord = woord;
            //console.log('begin');
            if ($scope.aantal != 0) {
                volgende = volgendeLetter(vorigeWoord.substring(actieveRij, actieveRij + 1));
                woord = draaiNieuweWoord(volgende);
                if (woord === eindstring) {
                    klaar = true;
                }
                else {
                    while (volgende === "a") {
                        //console.log('in while loop');
                        actieveRij++;
                        volgende = volgendeLetter(woord.substring(actieveRij, actieveRij + 1));
                        woord = draaiNieuweWoord(volgende);

                    }
                    actieveRij = 0;
                }
            }
            $scope.aantal++;
            return woord;
        }
    })
}());
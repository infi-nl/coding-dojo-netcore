(function () {
    "use strict";

    function Player(name) {
        const self = this;
        self.name = name;

        console.log("new player:", name);
    }

    function App() {
        const self = this;
        const dal = new DataAccessLayer("http://localhost:5000/api/v1/");
        const view = new View();
        let player;

        self.init = function() {
            view.intro.onUsername(start);
            view.level.onAnswer(handleAnswer);
            view.controls.onRestart(handleRestart);
            view.controls.onLogOut(handleLogOut);
        };

        function genericErrorHandler(error, message) {
            console.error(error);
            alert(message);
        }

        function start(name) {
            player = new Player(name);
            view.intro.hide();
            view.level.showWaiting();
            view.controls.showPlayerControls(name);

            dal.getCurrentLevel(player.name)
                .then(view.level.render, error => {
                    genericErrorHandler(error, "Er ging wat fout met het ophalen van het level. Probeer het opnieuw!");
                    view.level.hide();
                    view.intro.show();
                });
        }

        function showCurrentLevel() {
            view.level.showWaiting();

            dal.getCurrentLevel(player.name)
                .then(view.level.render, error => {
                    genericErrorHandler(error, "Er ging wat fout met het ophalen van het level. Probeer het opnieuw!");
                    view.level.showDetails();
                });
        }

        function handleAnswer(answer) {
            console.log("handleAnswer:", answer);

            dal
                .choose(player.name, answer.id)
                .then(
                    () => showCurrentLevel(),
                    error => {
                        genericErrorHandler(error, "Er ging wat fout met het versturen van je antwoord. Probeer het opnieuw!");
                        view.level.showDetails();
                    });
        }

        function handleRestart() {
            console.warn("Restarting!");

            dal.restart(player.name)
                .then(() => showCurrentLevel(), error => {
                    genericErrorHandler(error, "Restarting has failed.");
                });
        }

        function handleLogOut() {
            player = null;
            view.intro.show();
            view.level.hide();
            view.controls.hidePlayerControls();
        }
    }

    const app = new App();

    document.addEventListener("DOMContentLoaded", () => {
        app.init();
    });

})();
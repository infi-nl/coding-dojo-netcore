function View() {
    const self = this;
    self.intro = new Intro();
    self.level = new Level();
    self.controls = new Controls();

    function Controls() {
        const self = this;

        const restartCallbacks = [], logOutCallbacks = [];

        const dom = {
            playerControls: document.getElementById("playerControls"),
            playerName: document.getElementById("playerName"),
            logOut: document.getElementById("logOut"),
            restart: document.getElementById("restart"),
        };

        self.onRestart = (onRestartCallback) => {
            restartCallbacks.push(onRestartCallback);
        };

        self.onLogOut = (onLogOutCallback) => {
            logOutCallbacks.push(onLogOutCallback);
        };

        self.showPlayerControls = (playerName) => {
            dom.playerName.innerText = playerName;
            dom.playerControls.style.display = "inline-block";
        };

        self.hidePlayerControls = () => {
            dom.playerControls.style.display = "none";
        };

        dom.restart.addEventListener("click", (evt) => {
            evt.preventDefault();
            restartCallbacks.forEach(c => c());
        });

        dom.logOut.addEventListener("click", (evt) => {
            evt.preventDefault();
            logOutCallbacks.forEach(c => c());
        });
    }

    function Intro() {
        const self = this;

        const dom = {
            container: document.getElementById("intro"),
            name: document.getElementById("introName"),
            next: document.getElementById("introNext"),
        };

        const userNameCallbacks = [];

        self.onUsername = (onUsernameCallback) => {
            userNameCallbacks.push(onUsernameCallback);
        };

        self.show = () => {
            dom.container.style.display = "block";
        };

        self.hide = () => {
            dom.container.style.display = "none";
        };

        dom.next.addEventListener("click", (evt) => {
            evt.preventDefault();

            if (dom.name.value) {
                userNameCallbacks.forEach(cb => cb(dom.name.value));
            } else {
                alert("Please tell us your name.");
            }
        });
    }

    function Level() {
        const self = this;

        const dom = {
            container: document.getElementById("level"),

            waiting: document.getElementById("levelWaiting"),

            details: document.getElementById("levelDetails"),
            name: document.getElementById("levelName"),
            description: document.getElementById("levelDescription"),
            answers: document.getElementById("answers").getElementsByTagName("ol")[0]
        };

        const answerCallbacks = [];

        dom.answers.addEventListener("click", function (evt) {
            if (evt.target.tagName === "LI") {
                answerCallbacks.forEach(cb => cb(evt.target.dataset));
            }
        });

        self.onAnswer = function (onAnswerCallback) {
            answerCallbacks.push(onAnswerCallback);
        };

        self.showDetails = function () {
            dom.container.style.display = "block";
            dom.waiting.style.display = "none";
            dom.details.style.display = "block";
        };

        self.showWaiting = function () {
            dom.container.style.display = "block";
            dom.waiting.style.display = "block";
            dom.details.style.display = "none";
        };

        self.hide = function () {
            dom.container.style.display = "none";
        };

        self.render = function(level) {
            dom.name.innerHTML = level.levelName;
            dom.description.innerHTML = level.description;

            while (dom.answers.hasChildNodes()) {
                dom.answers.removeChild(dom.answers.childNodes[0]);
            }

            level.answers.forEach(answer => {
                let li = document.createElement("LI");
                li.innerHTML = answer.description;
                li.dataset.id = answer.id;
                dom.answers.appendChild(li);
            });

            self.showDetails();
        }
    }
}

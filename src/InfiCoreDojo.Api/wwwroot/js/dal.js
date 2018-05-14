function DataAccessLayer(baseUrl) {
    const self = this;
    
    self.getCurrentLevel = function(userName) {
        const levelData = get(`player/${userName}/current-level`);

        console.log(levelData);

        return levelData;
    };

    self.choose = function(userName, answerId) {
        return post("player/choose", {
            "playername" : userName,
            "answerId" : answerId
        });
    };

    self.restart = function (userName) {
        return post("player/restart", {
            "playerName": userName
        });
    };

    self.getLevelOverview = function() {
        return get("level/");
    };

    self.getLevelData = function(levelId) {
        return get(`level/${levelId}`);
    };

    self.modifyLevel = function(userName, level) {
        return post("level/", {
            "userName" : userName,
            "levelData" : level
        });
    };

    function post(url, body) {
        return window.fetch(baseUrl + url, {
            "body" : JSON.stringify(body),
            "headers" : {
                "content-type" : "application/json"
            },
            "method" : "POST"
        }).then(handleResponse);
    }

    function get(url) {
        return window.fetch(baseUrl + url, {
            "method" : "GET"
        }).then(handleResponse);
    }

    function handleResponse(response) {
        if (response.ok) {
            return response.json();
        }

        console.log(response);

        throw new Error("Server response was not ok");
    }
}

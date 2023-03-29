var sessionTimeout = 1 * 60 * 1000; 
var idleTimeout = 1 * 60 * 1000; 

var sessionTimer = setTimeout(function () {
    showLogoutMessage();
}, sessionTimeout);

var idleTimer = null;

function resetTimers() {
    clearTimeout(sessionTimer);

    if (idleTimer) {
        clearTimeout(idleTimer);
    }

    idleTimer = setTimeout(function () {
        showLogoutMessage();
    }, idleTimeout);

    sessionTimer = setTimeout(function () {
        showLogoutMessage();
    }, sessionTimeout);
}

function showLogoutMessage() {
    if (confirm("Your session is about to expire. Do you want to stay logged in?")) {
        resetTimers();
    } else {
        window.location.href = '/Login';
    }
}

$(document).on('mousemove keydown scroll', function () {
    resetTimers();
});
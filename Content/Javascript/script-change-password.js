//function for name field validation
function Checkusername() {
    let usernameinput = document.getElementById("username");

    if (usernameinput.value.trim() === "") {
        setError(usernameinput, "Empty username");
    }
    else {
        setSuccess(usernameinput);
    }
}

function Checkoldpassword() {
    let oldpasswordinput = document.getElementById("oldpassword");

    if (oldpasswordinput.value.trim() === "") {
        setError(oldpasswordinput, "Empty old password");
    }
    else {
        setSuccess(oldpasswordinput);
    }
}

function Checknewpassword() {
    let newpasswordinput = document.getElementById("newpassword");

    if (newpasswordinput.value.trim() === "") {
        setError(newpasswordinput, "Empty new password");
    }
    else {
        setSuccess(newpasswordinput);
    }
}

function Checkconfirmpassword() {
    let confirmpasswordinput = document.getElementById("confirmpassword");
    if (confirmpasswordinput.value.trim() === "") {
        setError(confirmpasswordinput, "Empty confirm password");
    }
    else {
        setSuccess(phoneinput);
    }
}

//till here
function Checkvalidation() {
    Checkusername();
    Checkoldpassword();
    Checknewpassword();
    Checkconfirmpassword();
}

function setError(input, message) {
    let submitbutton = document.getElementById("button")
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = "smallshown";
    small.innerText = message;
    submitbutton.disabled = true;
}

function setSuccess(input) {
    let submitbutton = document.getElementById("button")
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = "smallhidden";
    small.innerHTML = "";
    submitbutton.disabled = false;
}
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

function Checkpassword()
{
    var isPwd = /^[A-Za-z]\w{7,14}$/;
    let passwordinput = document.getElementById("password");
    if (passwordinput.value.trim() === "")
    {
        setError(passwordinput, "Empty password");
    }
    else if (!isPwd.test(passwordinput.value.trim())) {
        setError(passwordinput, 'Enter strong password');
    }
    else
    {
        setSuccess(passwordinput);
    }
}

//till here
function Checkvalidation() {
    Checkusername();
    Checkpassword();
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
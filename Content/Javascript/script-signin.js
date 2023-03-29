//function for name field validation
function Checkusername()
{
    let nameinput = document.getElementById("username");
    
    if (nameinput.value.trim() === "")
    {
        setError(nameinput, "Empty name");
    }
    else
    {
        setSuccess(nameinput);
    }
}

function Checkpassword()
{
    let pwdinput = document.getElementById("password");
    if (pwdinput.value.trim() === "")
    {
        setError(pwdinput, "Empty password");
    }
    else
    {
        setSuccess(pwdinput);
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
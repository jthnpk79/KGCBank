//Function for name field validation
function Checkfirstname()
{
    var isFname = /^[a-zA-Z]+$/;
    let fnameinput = document.getElementById("fname");
    
    if (fnameinput.value.trim() === "")
    {
        setError(fnameinput, "Empty first name");
    }
    else if (!isFname.test(fnameinput.value.trim()))
    {
        setError(fnameinput, 'Name cannot be a number or special characters');
    }
    else
    {
        setSuccess(fnameinput);
    }
}

//Lastname validation
function Checklastname() {
    var isLname = /^[a-zA-Z]+$/;
    let lnameinput = document.getElementById("lname");

    if (lnameinput.value.trim() === "") {
        setError(lnameinput, "Empty Last name");
    }
    else if (!isLname.test(lnameinput.value.trim())) {
        setError(lnameinput, 'Name cannot be a number or special characters');
    }
    else {
        setSuccess(lnameinput);
    }
}

//Gender validation
function Checkgender() {
    let fnameinput = document.getElementById("gender");

    if (fnameinput.value === "") {
        setError(fnameinput, "Empty Gender");
    }
    else {
        setSuccess(fnameinput);
    }
}

//Phone number validation
function Checkphone() {
    var isPhone = /^\d{10}$/;
    let phoneinput = document.getElementById("phone");

    if (phoneinput.value.trim() === "") {
        setError(phoneinput, "Empty phone number");
    }
    else if (!isPhone.test(phoneinput.value.trim())) {
        setError(phoneinput, 'Enter a valid number');
    }
    else {
        setSuccess(phoneinput);
    }
}

//Email validation
function Checkemail()
{
    var isEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    let emailinput = document.getElementById("email");
    
    if (emailinput.value.trim() === "")
    {
        setError(emailinput, "Empty email");
    }
    else if (!isEmail.test(emailinput.value.trim()))
    {
        setError(emailinput, 'Enter valid email');
    }
    else
    {
        setSuccess(emailinput);
    }
}

//Address validation
function Checkaddress()
{
    let addressinput = document.getElementById("address");

    if (addressinput.value.trim() === "") {
        setError(addressinput, "Empty Address");
    }
    else {
        setSuccess(addressinput);
    }
}
//City validation
function Checkcity() {
    var isCity = /^[a-zA-Z]+$/;
    let cityinput = document.getElementById("city");

    if (cityinput.value.trim() === "") {
        setError(cityinput, "Empty city");
    }
    else {
        setSuccess(cityinput);
    }
}
//State validation
function Checkstate() {
    var isName = /^[a-zA-Z]+$/;
    let nameinput = document.getElementById("state");

    if (nameinput.value.trim() === "") {
        setError(nameinput, "Empty state");
    }
    else {
        setSuccess(nameinput);
    }
}
//Pincode validation
function Checkpincode() {
    var isPincode = /^\d{6}$/;
    let pincodeinput = document.getElementById("pincode");

    if (pincodeinput.value.trim() === "") {
        setError(pincodeinput, "Empty pincode");
    }
    else if (!isPincode.test(pincodeinput.value.trim())) {
        setError(pincodeinput, 'Enter a valid number');
    }
    else {
        setSuccess(pincodeinput);
    }
}

//Username validation
function Checkusername()
{
    var isName = /^[a-zA-Z]+$/;
    let nameinput = document.getElementById("username");
    
    if (nameinput.value.trim() === "")
    {
        setError(nameinput, "Empty username");
    }
    else
    {
        setSuccess(nameinput);
    }
}

//Password validation
function Checkpassword()
{
    var isPwd = /^[A-Za-z]\w{7,14}$/;
    let pwdinput = document.getElementById("password");
    if (pwdinput.value.trim() === "")
    {
        setError(pwdinput, "Empty password");
    }
    else if (!isPwd.test(pwdinput.value.trim()))
    {
        setError(pwdinput, 'Enter strong password');
    }
    else
    {
        setSuccess(pwdinput);
    }
}

//Confirm password validation
function Checkconfirmpassword()
{
    let pwd2input = document.getElementById("confirmpassword");
    let pwdinput = document.getElementById("password");
    if (pwd2input.value.trim() === "")
    {
        setError(pwd2input, 'Empty password');
    }
    else if (pwd2input.value.trim() === pwdinput.value.trim())
    {
        setSuccess(pwd2input);
    }
    else
    {
        setError(pwd2input, "Password didn't match");
    }
}

//Date validation
function Checkdob()
{
    let dateinput = document.getElementById("dob");
    if (dateinput.value.trim() === "")
    {
        setError(dateinput, "Empty date of birth");
    }
    else if (!isDate.test(dateinput.value.trim()))
    {
        setError(dateinput, 'Enter valid date');
    }
    else
    {
        setSuccess(dateinput);
    }
}

//Future date disabled
var todayDate = new Date();
var month = todayDate.getMonth(); //04 - current month
var year = todayDate.getUTCFullYear(); //2021 - current year
var tdate = todayDate.getDate(); // 27 - current date 
if (month < 10) {
    month = "0" + month //'0' + 4 = 04
}
if (tdate < 10) {
    tdate = "0" + tdate; 
}
var maxDate = year + "-" + month + "-" + tdate;
document.getElementById("dob").setAttribute("max", maxDate);
document.getElementById("dob").setAttribute("min", "1900-01-01"); //age > 100+


//Submit button validation
function Checkvalidation()
{
    Checkfirstname();
    Checklastname();
    Checkgender();
    Checkphone()
    Checkemail();
    Checkaddress();
    Checkcity();
    Checkstate()
    Checkpincode();
    Checkusername();
    Checkpassword();
    Checkconfirmpassword();
    Checkdob();
}


//Functions for set errors
function setError(input, message)
{
    let submitbutton = document.getElementById("button")
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = "smallshown";
    small.innerText = message;
    submitbutton.disabled = true;
}

function setSuccess(input)
{
    let submitbutton = document.getElementById("button")
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = "smallhidden";
    small.innerHTML = "";
    submitbutton.disabled = false;
}

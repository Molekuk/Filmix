function setColor(name) {
    var style = document.getElementById(name).style;
    var value = parseFloat(document.getElementById(name).textContent);
    if (value >= 8)
        style.color = "green";
    else if (value > 5)
        style.color = "orange";
    else
        style.color = "red";
}
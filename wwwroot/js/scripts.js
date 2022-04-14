function setColor(name) {
    var style = document.getElementById(name).style;
    var value = parseFloat(document.getElementById(name).textContent);
    if (value >= 8)
        style.color = "#3ccb00";
    else if (value >= 7)
        style.color = "#a3fa00";
    else if (value >= 5)
        style.color = "#d6ce1c";
    else if (value >= 3)
        style.color = "#f17305";
    else if (value >= 0)
        style.color = "#D6331C";
}



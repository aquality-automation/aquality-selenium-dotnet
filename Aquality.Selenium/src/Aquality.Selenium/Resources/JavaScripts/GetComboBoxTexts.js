return (function(element) {
    let array = new Array();
    for (let i = 0; i < element.length; i++) {
        array.push(element[i].text);
    }
    return array;
})(arguments[0]);

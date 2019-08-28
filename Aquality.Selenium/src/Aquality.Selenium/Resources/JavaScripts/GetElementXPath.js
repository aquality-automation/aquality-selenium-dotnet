function getXpath(element) {
    if (element.tagName === 'HTML') {
        return '/html';
    }
    if (element === document.body) {
        return '/html/body';
    }
    // calculate position among siblings
    let position = 0;
    // Gets all siblings of that element.
    let siblings = element.parentNode.childNodes;
    for (let i = 0; i < siblings.length; i++) {
        let sibling = siblings[i];
        // Check sibling with our element if match then recursively call for its parent element.
        if (sibling === element) {
            return `${getXpath(element.parentNode)}/${element.tagName}[${position + 1}]`;
        }
        // if it is a sibling & element-node then only increments position.
        let type = sibling.nodeType;
        if (type === 1 && sibling.tagName === element.tagName) {
            position++;
        }
    }
}
return getXpath(arguments[0]);

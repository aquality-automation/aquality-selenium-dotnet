function getCssPath(element) {
    const selectorPath = [];

    while (element.tagName) {
        let selector = element.nodeName.toLowerCase();
        if (element.id) {
            selector += `#${element.id}`;
        } else if (element.parentNode) {
            let i = 0;
            const children = element.parentNode.children;
            while (i < children.length && children[i] !== element) {
                i++;
            }
            selector += `:nth-child(${i + 1})`
        }

        selectorPath.unshift(selector);
        element = element.parentNode;
    }

    return selectorPath.join(' > ');
}
return getCssPath(arguments[0]);

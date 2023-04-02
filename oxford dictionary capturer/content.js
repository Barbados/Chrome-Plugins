window.onload = function() {
    var apiUrl = 'http://localhost:5248/api/words/' + getWordName(); 
    var existingButton = document.getElementsByClassName('webtop')[0];
    var newElement = document.createElement('div');
    newElement.className = 'btn';
    let addMeLink = document.createElement('a');
    addMeLink.text = 'Add Me';
    addMeLink.id = 'add-to-mywordlist-link';
    // addMeLink.onclick = function() { doHttpGet(apiUrl); }
    addMeLink.addEventListener('click', function() { return doHttpGet(apiUrl); }, false);
    newElement.appendChild(addMeLink);
    // newElement.innerHTML = '<a id="add-to-mywordlist-link" onclick="return doHttpGet(\'' + apiUrl + '\');">Add me</a>';
    insertAfter(newElement, existingButton);

    function insertAfter(newNode, referenceNode) {
        referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
    }

    function getWordName() {
        return document.getElementsByClassName('headword')[0].innerText.replace(/ /g, '-');
    }

    function doHttpGet(url)
    {
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open("GET", url, true);
        addMeLink.text = 'Loading...';
        xmlHttp.onload = function (e) {
            if (xmlHttp.readyState === 4) {
                if (xmlHttp.status === 200) {
                    console.log(xmlHttp.responseText);
                }
            }
            addMeLink.text = 'Add Me';
        };
        xmlHttp.send(null);
        let responseResult = xmlHttp.responseText;

        return true;
    }
}
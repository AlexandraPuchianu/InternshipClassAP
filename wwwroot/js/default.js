let addNewComerBtn = document.getElementById("add-btn");
addNewComerBtn.addEventListener('click', addNewcomer);

function addNewcomer() {
    var newcomer = document.getElementById("newcomer");
    if (newcomer.value != '') {
        var node = document.createElement("li");
        node.innerText = newcomer.value;
        list.appendChild(node);
        newcomer.value = '';
    }

}
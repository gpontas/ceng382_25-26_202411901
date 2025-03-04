function activateAnimation() {
    let checkbox = document.getElementById("checkbox");
    checkbox.checked = false;

    setTimeout(() =>{
        checkbox.checked = true;
    },1);

}
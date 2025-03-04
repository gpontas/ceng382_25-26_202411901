
let tuples = [];

function addTuple(item1, item2) {
    tuples.push([item1, item2]);
    console.log("Added tuple:", [item1, item2]);
}

const loginForm = document.getElementById("loginForm");

// event listener structure taken from DeepSeek LLM 
loginForm.addEventListener("submit", function (event) {
    event.preventDefault(); 
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    addTuple(username, password);
    loginForm.reset();
});

;
  
const form = document.getElementById('classForm');
const tableBody = document.getElementById('classTableBody');

form.addEventListener('submit', (e) => {
    e.preventDefault();
    const className = document.getElementById('className').value;
    const numPeople = document.getElementById('numPeople').value;
    const description = document.getElementById('description').value;

    if (!className || !numPeople || !description) {
        alert('Please fill all fields');
        return;
    }

    addClassToTable(className, numPeople, description);
    form.reset();
});

function addClassToTable(name, people, desc) {
    const newRow = document.createElement('tr');
    
    newRow.innerHTML = `
        <td>${name}</td>
        <td>${people}</td>
        <td>${desc}</td>
    `;
    newRow.addEventListener('click', highlightRow);
    newRow.addEventListener('click', showAlert);
    newRow.addEventListener('dblclick', () => removeRow(newRow));
    newRow.addEventListener('mouseover', () => newRow.style.backgroundColor = '#f0f0f0');
    newRow.addEventListener('mouseout', () => newRow.style.backgroundColor = '');
    tableBody.appendChild(newRow);
}
function highlightRow(e) {
    const rows = document.querySelectorAll('.class-table tr');
    rows.forEach(row => row.classList.remove('active'));
    
    const [name, people, desc] = e.target.parentNode.children;
    console.log({
        className: name.textContent,
        numPeople: people.textContent,
        description: desc.textContent
    });
    
    e.target.parentNode.classList.add('active');
}

function removeRow(row) {
    row.remove();
    console.log('Row removed');
}

function showAlert(event) {
    const row = event.currentTarget;
    const name = row.children[0].textContent;
    const people = row.children[1].textContent;
    const desc = row.children[2].textContent;

    alert(`Name: ${name}\nPeople: ${people}\nDescription: ${desc}`);
}

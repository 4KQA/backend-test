$(document).ready(function () {
    getPeople();
})


function getPeople() {
    ur = 'https://localhost:7289/api/people/all'
    $.ajax({
        url: ur,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            $("#peopleList").empty();
            showPeople(data);
        },
        error: function (error) {
            if (error) {
                console.log(error);
            }
        }
    });
}

function searchLastName() {
    let lastName = document.getElementById("searchbar").value
    if (lastName == "") {
        getPeople();
        return
    }
    ur = 'https://localhost:7289/api/people/search'
    $.ajax({
        url: ur,
        type: 'GET',
        dataType: 'json',
        data: {
            "lastName": lastName
        },
        success: function (data) {
            console.log(data);
            $("#peopleList").empty();
            showPeople(data);
        },
        error: function (error) {
            if (error) {
                console.log(error);
            }
        }
    });
}

function showPeople(data) {
    data.forEach(data => {

        $('#peopleList').append(
            `<tr>
			<td id="first${data.id}">${data.firstName}</td>
			<td id="last${data.id}">${data.lastName}</td>
			<td id="age${data.id}">${data.age}</td>
			<td id="gender${data.id}">${data.gender}</td>
			<td id="lon${data.id}">${data.longitude}</td>
			<td id="lat${data.id}">${data.latitude}</td>
			<td id="alive${data.id}">${data.alive}</td>
            <td><button id="${data.id}" onclick="editPerson(this.id)">Edit</button></td>
            </tr>`
        );
    })
}

function editPerson(id) {
    console.log(id);

    document.getElementById("idE").value = id;
    document.getElementById("firstE").value = document.getElementById("first" + id).innerHTML;
    document.getElementById("lastE").value = document.getElementById("last" + id).innerHTML;
    document.getElementById("ageE").value = document.getElementById("age" + id).innerHTML;
    document.getElementById("genderE").value = document.getElementById("gender" + id).innerHTML;
    document.getElementById("lonE").value = document.getElementById("lon" + id).innerHTML;
    document.getElementById("latE").value = document.getElementById("lat" + id).innerHTML;
    document.getElementById("statusE").value = document.getElementById("alive" + id).innerHTML;

}

function showStats(data) {

    $('#stats').append(
        `<label id="survivors">Survivors: ${data.survivors}</label>
        <br />
        <label id="deceased">Deceased: ${data.deceased}</label>
        <br />
        <label id="pctAlive">Percent alive: ${data.pctageOfsurvivors}%</label>
        <br />
        <label id="pctNorth">Percent of all people on northern hemisphere: ${data.pctOnNorthHem}%</label>
        <br />
        <label id="pctSouth">Percent of all people on southern hemisphere: ${data.pctONSouthHem}%</label>
        `
    );
}



function getStats() {
    ur = 'https://localhost:7289/api/people/stats'
    $.ajax({
        url: ur,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            showStats(data);
        },
        error: function (error) {
            if (error) {
                console.log(error);
            }
        }
    });
}




function createPerson() {
    document.getElementById("idE").value = -1;

}


function update() {
    let rType;
    let ur;
    let id = document.getElementById("idE").value;

    if (id == -1) {
        ur = "https://localhost:7289/api/people";
        rType = "POST";
    }
    else {
        ur = "https://localhost:7289/api/people/" + id;
        rType = "PUT";
    }

    var person = new Object();
    person.id = id;
    person.firstName = document.getElementById("firstE").value;
    person.lastName = document.getElementById("lastE").value;
    person.age = document.getElementById("ageE").value;
    person.gender = document.getElementById("genderE").value;
    person.longitude = document.getElementById("lonE").value;
    person.latitude = document.getElementById("latE").value;
    person.alive = document.getElementById("statusE").value;
    

    $.ajax({
        url: 'https://localhost:7289/api/people/update',
        method: 'PUT',
        dataType: 'json',
        data: person,
        success: function () {
            console.log();
            getPeople();
        },
        error: function (error) {
            if (error) {
                console.log(error);
            }
        }
    });

}
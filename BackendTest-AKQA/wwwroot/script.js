$(document).ready(function () {
    getPeople();
    $('#searchBtn').click(function () {
        searchLastName();
    })

    getPeople();
    $('#update').click(function () {
        update();
    })

    getPeople();
    $('#clearInput').click(function () {
        clearInput();
    })
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
    }).done(function () {
        getStats();
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
            $("#stats").empty();
            showStats(data);
        },
        error: function (error) {
            if (error) {
                console.log(error);
            }
        }
    });
}


async function update() {
    let id = document.getElementById("idE").value;
    if (id === undefined) {
        id = -1;
    }

    let person = getPersonInfo(id);

    //Sees if person needs to be updated or created(i=-1)
    if (id == -1) {
        postRequest(person);
    }
    else {
        putRequest(person);
    }

}

function putRequest(data) {
    $.ajax({
        url: 'https://localhost:7289/api/people/update',
        type: 'PUT',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        error: function (error) {
            if (error) {
                console.log(error);
            }
        },
        success: function (response) {
            console.log(response);
        }
    }).done(function () {
        getPeople();
        clearInput();
        getStats();
    });
}


function postRequest(data) {
    $.ajax({
        url: 'https://localhost:7289/api/people/create',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        error: function (error) {
            if (error) {
                console.log(error);
            }
        },
        success: function () {
            console.log("Success");
        }
    }).done(function () {
        getPeople();
        clearInput();
        getStats();
    });
}

function clearInput() {
    document.getElementById("idE").value = -1;
    document.getElementById("firstE").value = "";
    document.getElementById("lastE").value = "";
    document.getElementById("ageE").value = "";
    document.getElementById("genderE").value = "Female";
    document.getElementById("lonE").value = "";
    document.getElementById("latE").value = "";
    document.getElementById("statusE").value = "true";
}

function getPersonInfo(id) {

    var person = new Object();

    person.id = id;
    person.FirstName = document.getElementById("firstE").value;
    person.lastName = document.getElementById("lastE").value;
    person.age = document.getElementById("ageE").value;
    person.gender = document.getElementById("genderE").value;
    person.longitude = document.getElementById("lonE").value;
    person.latitude = document.getElementById("latE").value;
    person.alive = document.getElementById("statusE").value;

    var live = (person.alive === 'true')
    person.alive = live;
    return person;
}
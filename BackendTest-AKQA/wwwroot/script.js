const Http = new XMLHttpRequest();
$(document).ready(function () {
    ss();
})

function ss() {
    ur = 'https://localhost:7289/api/people/all'
    $.ajax({
        url: ur,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
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
        ss();
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
			<td>${data.firstName}</td>
			<td>${data.lastName}</td>
			<td>${data.age}</td>
			<td>${data.gender}</td>
			<td>${data.longitude}</td>
			<td>${data.latitude}</td>
			<td>${data.alive}</td>
            </tr>`
        );
    })
}


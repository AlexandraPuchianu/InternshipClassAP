"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();


connection.on("AddMember", function (name, id) {
    // Remember string interpolation
    $("#list").append(`<li class="member" memberId="${id}">
                    <div class="memberInfo">
	                <span class="name">${name}</span><span class="delete fa fa-remove"></span>
                    <i class="startEdit fa fa-pencil" data-toggle="modal" data-target="#editClassmate"></i>
                    </div>
                    </li>`);
});

connection.on("RemoveMember", function (id) {
    var $li = $(`li.member[memberId=${id}]`);
    $li.remove();
});

connection.on("UpdateMember", function (name, id) {
    var nameTag = $(`li.member[memberId=${id}] .memberInfo .name`);
    nameTag.text(name);
});


connection.start().then(function () {
    console.log("Connection established");
}).catch(function (err) {
    return console.error(err.toString());
});
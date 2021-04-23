(function () {
    var txtMessage = $('#txtMessage')
    var btnSend = $('#btnSend')
    var listMessages = $('#listMessages')
    var userName = $('#userName').val();

    var connection = new signalR.HubConnectionBuilder().withUrl('/chathub').build();

    $(btnSend).click(function () {
        var userMessage = $(txtMessage).val();

        connection.invoke('SendMessage', {
            userName: userName,
            message: userMessage
        }).catch(function (error) {
            alert("Can't send a message");
            console(error);
        })

        $(txtMessage).val('');
    });

    connection.start().then(function () {
        console.log('Connected to SignalR hub');
        $(btnSend).removeAttr('disabled');

        connection.invoke('RegisterUser', userName).catch(function (error) {
            console.error(error);
        });
    })

    connection.on('ReceiveMessage', function (obj) {
        $(listMessages).prepend('<li>['
            + obj.timeStampString + ']'
            + '<span class="font-weight-bold">user: '
            + obj.username
            + '</span> | message: '
            + obj.message
            + '</li>')
    });

    connection.on('UserJoined', function (userName) {
        $(listMessages).prepend('<li class="font-weight-bold text-success">User '
            + userName
            + ' joined to the conversation</li>')
    });

})();

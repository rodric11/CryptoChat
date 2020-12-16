$(function () {

    var chat = $.connection.chatHub;

    chat.client.broadcastMessage = function (name, message, key) {

        if ($('#mykey').val == "") {
            $('#mykey').val(key.toString());
        }

        // Ddecrypting
        var decrypted = CryptoJS.AES.decrypt(message, key);
        //var decrypted = message;

        var encodedName = $('<div />').text(name).html();
        var encodedMsg = $('<div />').text(decrypted.toString(CryptoJS.enc.Utf8)).html();

        $('#discussion').append('<li><strong>' + encodedName
            + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };


    $('#displayname').val(prompt('Enter your name:', ''));
    $('#message').focus();

    // Start the connection.       
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            // Crypting
            var mykey = "";

            if ($('#mykey').val() == "") {
                mykey = Math.random().toString(36).substring(7);
            }

            var encrypted = CryptoJS.AES.encrypt($('#message').val(), mykey.toString());

            chat.server.send($('#displayname').val(), encrypted.toString(), mykey.toString());

            $('#message').val('').focus();
        });
    });
});
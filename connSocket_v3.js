function connSocket_v3()
{
    try{
        var client = Stomp.client('wss://'+location.host+'/ntf/ws');
        client.heartbeat.outgoing = 10000;
        client.heartbeat.incoming = 0;
        var headers = {
            login: 'notification',
            passcode: 'notification'
          };
        client.reconnect_delay = 1000;
        client.debug = function(e) {};
        var on_connect = function(x) {
            id = client.subscribe('/exchange/agent-#currentUser.username#', function(m) {
                
                xlSocket(m.body);
            });
            console.log("connected: " + client.connected);
        };
      var on_error = function(error) {
        console.log("error socket");
      };
      client.connect(headers, on_connect, on_error);
    }catch(e){console.log("catch e: ",e)}
}
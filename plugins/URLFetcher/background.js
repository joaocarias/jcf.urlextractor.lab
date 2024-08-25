function connectToSocket() {
    let socket = new WebSocket("ws://localhost:19019/");
  
    socket.onopen = function() {
      console.log("Conectado ao WebSocket");
      socket.send("getUrl");
    };
  
    socket.onmessage = function(event) {
      const message = event.data;
      console.log("Mensagem recebida do servidor: ", message);
  
      if (message === "Solicitação de URL recebida") {
        chrome.tabs.query({ active: true, currentWindow: true }, (tabs) => {
          const activeTab = tabs[0];
          const url = activeTab.url;
          socket.send(url);
        });
      }
    };
  
    socket.onclose = function(event) {
      console.log("WebSocket fechado: ", event);
    };
  
    socket.onerror = function(error) {
      console.error("Erro no WebSocket: ", error);
    };
  }
  
  connectToSocket();
  
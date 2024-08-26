

// Monitora quando a navegação em uma aba começa (antes do carregamento completo)
chrome.webNavigation.onBeforeNavigate.addListener(function(details) {
  if (details.frameId === 0) {
      sendUrlToServer(details.url);
  }
});

// Monitora quando uma aba é ativada (usuário muda de aba)
chrome.tabs.onActivated.addListener(function(activeInfo) {
  chrome.tabs.get(activeInfo.tabId, function(tab) {
      sendUrlToServer(tab.url);
  });
});

// Monitora quando o usuário muda o foco entre janelas
chrome.windows.onFocusChanged.addListener(function(windowId) {
  if (windowId !== chrome.windows.WINDOW_ID_NONE) {
      chrome.tabs.query({active: true, windowId: windowId}, function(tabs) {
          if (tabs.length > 0) {
              sendUrlToServer(tabs[0].url);
          }
      });
  }
});

function sendUrlToServer(url) {
  if (url && url.trim() !== '') {
    fetch('http://localhost:18018/api/urlextractor/url', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ url: url })
    }).then(response => {
        if (response.ok) {
            console.log('URL enviada com sucesso:', url);
        } else {
            console.error('Erro ao enviar a URL:', response.statusText);
        }
    }).catch(error => {
        console.error('Erro ao enviar a URL:', error);
    });
  }
}

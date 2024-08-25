document.getElementById('fetch-url').addEventListener('click', () => {
    chrome.runtime.sendMessage({ type: "getUrl" }, (response) => {
      document.getElementById('current-url').innerText = response.url;
    });
  });
  
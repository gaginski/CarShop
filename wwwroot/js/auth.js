window.carShopLogin = {
    login: async function (model) {
        const response = await fetch('/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                userName: model.username,
                password: model.password
            }),
            credentials: 'include' 
        });

        try {
            const text = await response.text();
            console.log('Resposta /auth/login:', response.status, text);
        } catch (e) {
            console.log('Erro lendo resposta:', e);
        }

        return response.status;
    }
}; 
window.carShopLogout = function () {
    document.cookie = "AuthToken=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/";
}

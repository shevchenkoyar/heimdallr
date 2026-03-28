window.auth = {
    login: async function (login, password) {
        try {
            const response = await fetch('/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
                body: JSON.stringify({
                    login: login,
                    password: password
                })
            });

            return response.ok;
        } catch (error) {
            console.error('Login error:', error);
            return false;
        }
    }
};
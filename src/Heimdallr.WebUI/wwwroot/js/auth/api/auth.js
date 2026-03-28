window.auth = async function (login, password) {
    try {
        const response = await fetch('/api/user/auth', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify({
                username: login,
                password: password
            })
        });

        return response.ok;
    } catch (error) {
        console.error('Login error:', error);
        return false;
    }
}
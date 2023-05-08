export class TokenHandler {

    setToken(token: string) {
        localStorage.setItem('token', token);
    }

    getToken(): string | null {
        return localStorage.getItem('token');
    }

    removeToken() {
        localStorage.removeItem('token');
    }

    getDecodedToken(): any {
        const token = this.getToken();

        if (!token) {
            return null
        }

        const payload = window.atob(token.split('.')[1]);
        const parsedToken = JSON.parse(payload);
        return parsedToken;
    }

    getNameFromToken(): any {
        const parsedToken = this.getDecodedToken();
        return parsedToken.Name;
    }

    getEmailFromToken(): any {
        const parsedToken = this.getDecodedToken();
        return parsedToken.Email;
    } 
    
    getRoleFromToken(): any {
        const parsedToken = this.getDecodedToken();
        return parsedToken.Role;
    }

    getUserIdFromToken(): any {
        const parsedToken = this.getDecodedToken();
        return parsedToken.UserId;
    }
}
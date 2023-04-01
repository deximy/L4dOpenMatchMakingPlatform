import * as jose from "jose";

const IsAuthenticated = async () => {
    let token = localStorage.getItem("token");
    if (token === null)
    {
        return false;
    }

    await jose.jwtVerify(token, new TextEncoder().encode('a'.repeat(256)));

    return true;
};

const GetUserIdFromToken = async () => {
    let token = localStorage.getItem("token");
    if (token === null)
    {
        return "";
    }

    let claims = await jose.jwtVerify(token, new TextEncoder().encode('a'.repeat(256)));
    return claims.payload.nameid as string;
};

export {IsAuthenticated, GetUserIdFromToken};

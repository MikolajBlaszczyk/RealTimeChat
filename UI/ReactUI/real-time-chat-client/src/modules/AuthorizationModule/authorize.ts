const LOGIN_URL = process.env.API_LOGIN as string 


export async function AuthorizeUser(username :string, password :string) : Promise<Response>
{
    const data = { 
        Username: username,
        Password: password
    }    

    const response =  await fetch(LOGIN_URL,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })

    const result = await response.json();
    return result;
}
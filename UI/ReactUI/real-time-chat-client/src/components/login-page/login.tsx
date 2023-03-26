import styles from "../../styles/Authentication.module.css"
import { ChangeEvent, FormEvent, useState, useEffect  } from "react"
import { AuthorizeUser } from "../../modules/AuthorizationModule/authorize"
import { useRouter } from "next/router"

const INPUT_TEXT = "text"
const INPUT_PASSWORD = "password"




export default function Login(){
    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const [response, setResponse] = useState<Response>()

    const textInput = `${styles.inputOne} ${styles.button}`
    const passwordInput = `${styles.inputTwo} ${styles.button}`
    const submitButton = `${styles.inputThree} ${styles.button}`
    
    useEffect(() =>{
        if(response != null){
            if(response.ok){
                useRouter
            }
            else{
                alert('failed to authorize')
            }
        }
    },[response])


    const onFormSubmit = async (event :FormEvent<HTMLFormElement>) =>{
        event.preventDefault()

        const responseData = await AuthorizeUser(login, password)
        setResponse(responseData)
    }

    const onInputChange = (event :ChangeEvent<HTMLInputElement>) =>{
        const input = event.target;
        
        switch(input.type){
            case INPUT_TEXT:
                setLogin(input.value)
                break;
            case INPUT_PASSWORD:
                setPassword(input.value)
                break;
        }
    }

    return (
        <form className={styles.loginForm} onSubmit={onFormSubmit}>
            <input type="text" placeholder="login" className={textInput} value={login} onChange={onInputChange}/>
            <input type="password" placeholder="password" className={passwordInput} value={password} onChange={onInputChange}/>
            <input type='submit' className={submitButton} value="Login"/>
        </form>
    )
}
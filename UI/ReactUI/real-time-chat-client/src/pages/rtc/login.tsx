import styles from "../../styles/Authentication.module.css"
import LoginComponent from "../../components/login-page/login"

export default function Login(){

    return(
        <main>
            <div className={styles.authenticationLayout}>
                <div className={styles.mainDiv}>
                    <LoginComponent/>
                </div>
            </div>    
        </main>
    )
}
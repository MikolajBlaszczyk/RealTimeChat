import { useRouter } from "next/router";
import styles from "../../styles/Start.module.css"



export default function Start()
{
    const router = useRouter()

    const guide :string= "Welcome to RTC, Login to chat or register if you doesn't have an account"
    const firstButtonClasses:string = `${styles.login} ${styles.button}`
    const secondButtonClasses:string = `${styles.register} ${styles.button}`

    const LoginButtonOnClick = () =>{
        router.push('/rtc/login')       
    }
    const RegisterButtonOnClick = () =>{
        router.push('/rtc/register')
    }

    return(
    <div className={styles.start}>
        <span className={styles.guide}>{guide}</span>
        <button className={firstButtonClasses} onClick={LoginButtonOnClick}>Login</button>
        <button className={secondButtonClasses} onClick={RegisterButtonOnClick}>Register</button>
        {/* Logo */}
    </div>
    )
}
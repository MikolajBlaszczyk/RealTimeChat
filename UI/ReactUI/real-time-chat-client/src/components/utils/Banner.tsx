import styles from "../../styles/Utils.module.css"

interface IBannerProperty{
    title :String,
    description :String
}

export default function Banner({title, description}: IBannerProperty){    
    
    return (
        <div className={styles.banner}>
            <span className={styles.title}>{title}</span>
            <span className={styles.description}>{description}</span>
        </div>
    )
}
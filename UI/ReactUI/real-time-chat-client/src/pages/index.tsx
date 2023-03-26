import Start from '../components/start-page/Start'
import Banner from '../components/utils/Banner'
import { Inter } from 'next/font/google'
import styles from '@/styles/Home.module.css'

const inter = Inter({ subsets: ['latin'] })

export default function Home() {
  return (
    <>
      <main className={styles.main}>
          <div className={styles.startLayout}>
            <div className={styles.startContent}>
              <Start/>
            </div>
            <div className={styles.bannerContent}>
              <Banner title="RTC" description="Some description"/>
            </div>
          </div>
      </main>
    </>
  )
}

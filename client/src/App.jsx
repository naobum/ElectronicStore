import { useState } from 'react'
import './App.css'
import { CartProvider } from './context/CartContext'
import Products from './pages/Products'
import Cart from './pages/Cart'
import Admin from './pages/Admin'

function App() {
  const [currentPage, setCurrentPage] = useState('products')

  const renderPage = () => {
    switch (currentPage) {
      case 'products':
        return <Products />
      case 'cart':
        return <Cart />
      case 'admin':
        return <Admin />
      default:
        return <Products />
    }
  }

  return (
    <CartProvider>
      <div className="app">
        <nav className="navbar">
          <div className="navbar-container">
            <h1 className="logo">ElectronicStore</h1>
            <ul className="nav-links">
              <li>
                <button
                  className={`nav-btn ${currentPage === 'products' ? 'active' : ''}`}
                  onClick={() => setCurrentPage('products')}
                >
                  Товары
                </button>
              </li>
              <li>
                <button
                  className={`nav-btn ${currentPage === 'cart' ? 'active' : ''}`}
                  onClick={() => setCurrentPage('cart')}
                >
                  Корзина
                </button>
              </li>
              <li>
                <button
                  className={`nav-btn ${currentPage === 'admin' ? 'active' : ''}`}
                  onClick={() => setCurrentPage('admin')}
                >
                  Админка
                </button>
              </li>
            </ul>
          </div>
        </nav>

        <main className="main-content">
          {renderPage()}
        </main>
      </div>
    </CartProvider>
  )
}

export default App

import { BrowserRouter, NavLink, Route, Routes } from 'react-router-dom';
import { CartProvider } from './context/CartContext.jsx';
import Products from './pages/Products.jsx';
import Cart from './pages/Cart.jsx';
import Admin from './pages/Admin.jsx';
import './styles/App.css';

export default function App() {
  return (
    <CartProvider>
      <BrowserRouter>
        <div className="app-shell">
          <header className="app-header">
            <div className="app-title">Electronic Store</div>
            <nav className="app-nav">
              <NavLink to="/" end className={({ isActive }) => (isActive ? 'nav-link active' : 'nav-link')}>
                Товары
              </NavLink>
              <NavLink to="/cart" className={({ isActive }) => (isActive ? 'nav-link active' : 'nav-link')}>
                Корзина
              </NavLink>
              <NavLink to="/admin" className={({ isActive }) => (isActive ? 'nav-link active' : 'nav-link')}>
                Админ
              </NavLink>
            </nav>
          </header>
          <main className="app-content">
            <Routes>
              <Route path="/" element={<Products />} />
              <Route path="/cart" element={<Cart />} />
              <Route path="/admin" element={<Admin />} />
            </Routes>
          </main>
        </div>
      </BrowserRouter>
    </CartProvider>
  );
}

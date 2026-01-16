import './App.css';
import {Toaster} from "react-hot-toast"; // npm install react-hot-toast
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Register from './components/Auth/Register';
import Login from './components/Auth/Login';
import NotFound from './pages/NotFound';

function App() {
  return (
    <BrowserRouter>
      <Toaster position='top-center' toastOptions={{ success: { duration: 2000 }, error: { duration: 2000 } }} />
      <Routes>
        <Route path="/register" element={<Register />} />
        <Route path="/login" element={<Login />} />

        <Route path='/*' element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;

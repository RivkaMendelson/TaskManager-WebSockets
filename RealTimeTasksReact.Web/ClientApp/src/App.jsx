import React from 'react';
import { Route, Routes } from 'react-router-dom';
import AuthContextComponent from './AuthContext';
import Layout from './Layout';
import Home from './Home';
import Logout from './Logout';
import PrivateRoute from './PrivateRoute';
import Login from './Login';
import Signup from './Signup';



const App = () => {
    return (
        <AuthContextComponent>
            <Layout>
                <Routes>

                    <Route exact path='/' element={
                        <PrivateRoute>
                            <Home />
                        </PrivateRoute>} />
                    <Route exact path='/signup' element={<Signup />} />
                    <Route exact path='/login' element={<Login />} />
                    <Route exact path='/logout' element={
                        <PrivateRoute>
                            <Logout />
                        </PrivateRoute>} />
                </Routes>
            </Layout>
        </AuthContextComponent>
    );
}

export default App;
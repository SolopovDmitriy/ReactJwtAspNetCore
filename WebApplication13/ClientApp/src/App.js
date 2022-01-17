import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import { Employees } from './components/Employees';
import { Categories } from './components/Categories';
import { OneEmployee } from './components/OneEmployee';
import { OneCategory } from './components/OneCategory';
import { OnePost } from './components/OnePost';
import { Posts } from './components/Posts';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/showEmployees' component={Employees} />
        <Route path='/showCategories' component={Categories} />
        <Route path='/showOneEmployee/:id?' component={OneEmployee} />
        <Route path='/showOneCategory/:id?' component={OneCategory} />
        <Route path='/showOnePost/:id?' component={OnePost} />
        <Route path='/showPosts' component={Posts} />
      </Layout>
    );
  }
}

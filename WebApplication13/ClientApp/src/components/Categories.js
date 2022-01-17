import React, { Component } from 'react';

export class Categories extends Component {

    constructor(props) {
        super(props);
        this.state = {
            сategories: [],
            loading: true
        };
    }

    componentDidMount() {
        this.populateСategoriesData();
    }
    static renderCategoriesCards(сategories) {
        return (
            <div className='container'>

                <div className='row'>
                    {сategories.map(category =>
                        <div className="mb-2 col-md-4 ">
                            <div className="card pt-3" key={category.id}>
                                <div className=''>
                                    <img className="card-img-top" src={category.imgSrc} alt={category.imgAlt} />
                                    <div className="card-body">
                                        <h5 className="card-title text-center">{category.title}</h5>
                                        <p className="card-text text-justify">{category.slogan}</p>
                                        <div className="form-row text-center">
                                            <div className="col-12 text-white">
                                                <a href={"/showOneCategory/" + category.urlSlug} className="btn btn-primary">Подробнее...</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Categories.renderCategoriesCards(this.state.сategories);

        return (
            <div>
                <h1 id="tabelLabel" className='text-center'>Все Категории</h1>
                <p className='text-center'>Чтото там.....</p>
                {contents}
            </div>
        );
    }

    async populateСategoriesData() {                                     //методзапроса на сервер
        const response = await fetch('Category', {
            method: 'GET'
        });
        const data = await response.json();                             //ответ конвертим в json
        console.log(data);
        this.setState({ сategories: data, loading: false });             //меняем состояние обьекта state - инитим forecasts массив данными с сервера
    }
}

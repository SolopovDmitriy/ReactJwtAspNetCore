import React, { Component } from 'react';

export class OneCategory extends Component {

    constructor(props) {
        super(props);
        this.state = {
            category: null,                    //массив с сотрудниками
            loading: true
        };
    }

    componentDidMount() {
        this.populateOneCategoryData();
    }
    static renderCategoryCard(category) {
        return (
            <div className='container'>
                <div className='row'>
                    <div className="card col" key={category.id}>
                        <div className=''>
                            <img className="mx-auto d-block w-25" src={category.imgSrc} alt={category.imgAlt} />
                                <div className="card-body">
                                <h5 className="card-title text-center">{category.title}</h5>
                                <p className="card-text text-justify">{category.slogan}</p>
                                <div className="form-row text-center">
                                    {category.content}
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : OneCategory.renderCategoryCard(this.state.category);
        let title = (this.state.category == null) ? null : this.state.category.title
        return (
            <div>
                <h1 id="tabelLabel" className='text-center'>Категория: {title} </h1>
                <p className='text-center'>Всегда на страже здоровья ваших питомцев</p>
                {contents}
            </div>
        );
    }

    async populateOneCategoryData() {                                     //методзапроса на сервер
        let catId = encodeURIComponent(this.props.match.params.id);
        console.log(catId);

        const response = await fetch('Category/' + catId, {
            method: 'GET'
        });
        console.log(response);
        const data = await response.json();                             //ответ конвертим в json
        console.log(data);
        this.setState({ category: data, loading: false });             //меняем состояние обьекта state - инитим forecasts массив данными с сервера
    }
}

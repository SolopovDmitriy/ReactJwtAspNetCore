import React, { Component } from 'react';

export class OnePost extends Component {

    constructor(props) {
        super(props);
        this.state = {
            post: null,                    
            loading: true
        };
    }

    componentDidMount() {
        this.populateOnePostData();
    }
    static renderOnePostCard(post) {
        return (
            <div className='container'>
                <div className='row'>
                    <div className="card col" key={post.id}>
                        <h1 id="tabelLabel" className='text-center'>{post.title}</h1>
                        <div className=''>
                            <img className="mx-auto card-img-top" src={post.imgSrc} alt={post.imgAlt}/>
                                <div className="card-body">
                                <p className="card-text text-justify">{post.slogan}</p>
                                <div className="form-row text-center">
                                    {post.content}
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
            : OnePost.renderOnePostCard(this.state.post);

        return (
            <div>
                {contents}
            </div>
        );
    }

    async populateOnePostData() {                                     //метод запроса на сервер
        let postSlug = encodeURIComponent(this.props.match.params.id);
        console.log(postSlug);

        const response = await fetch('Post/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postSlug),
        });
        console.log(response);
        const data = await response.json();                             //ответ конвертим в json
        console.log(data);
        this.setState({ post: data, loading: false });             //меняем состояние обьекта state - инитим forecasts массив данными с сервера
    }
}

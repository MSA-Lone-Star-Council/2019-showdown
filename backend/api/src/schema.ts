const schema : string = `
    type Viewer {
        name: String
    }

    type Token {
        token: String
        error: String
    }

    type Query {
        viewer(token: String) : Viewer
    }
    type Mutation {
        createToken(facebookAccessToken: String!) : Token
    }

    schema {
        query: Query
        mutation: Mutation
    }
`;

export default schema;
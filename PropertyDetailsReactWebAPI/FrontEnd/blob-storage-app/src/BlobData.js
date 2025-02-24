import React, { useEffect, useState } from 'react';
import axios from 'axios';

const BlobData = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get('http://localhost:5298/Blob/read-json');
                setData(response.data);
                } catch (error) {
                setError(error);
                setLoading(false);
            }
        };

        fetchData();
    }, []);


    if (error) {
        return <div>Error: {error.message}</div>;
    }

    return (
        <div>
            <h1>Blob Data</h1>
            {data.map((property) => (
                <div key={property.PropertyId}>
                   <h2>{property.PropertyName}</h2>
                    <p>Features: {property.features.join(', ')}</p>
                    <p>Highlights: {property.highlights.join(', ')}</p>
                    <h3>Transportation Options:</h3>
                    {property.TransportationOptions ? (
                        property.TransportationOptions.map((transport, index) => (
                            <div key={transport+index}>
                                <p>Type: {transport.Type}</p>
                                <p>Line: {transport.Line}</p>
                                <p>Distance: {transport.Distance}</p>
                            </div>
                        ))
                    ) : (
                        <p>None</p>
                    )}
                    <h3>Spaces:</h3>
                    {property.spaces.map((space) => (
                        <div key={space.spaceId}>
                            <p>Space Name: {space.spaceName}</p>
                            {space.rentRoll.map((rentRoll, index) => (
                                <div key={index}>
                                    <p>Month: {rentRoll.month}</p>
                                    <p>Rent: {rentRoll.rent}</p>
                                </div>
                            ))}
                        </div>
                    ))}
                </div>
            ))}
            
        </div>
    );
};

export default BlobData;